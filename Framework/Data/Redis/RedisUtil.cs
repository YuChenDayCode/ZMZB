using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Data.Redis
{
    public class RedisUtil
    {
        /*
         * 
         * Add():只能在key没有时进行添加，已存add无法生效
         * Expire():生存时间秒   Pexpire():生存时间毫秒
         * ExpireAt():timespan秒级时间戳    PexpireAt():timespan毫秒级时间戳
         * ExpireEntryAt():指定时间DateTime过期   ExpireEntryIn():指定时间TimeSpan到期时间
         * 
         * 注意：string的set之后生命周期会重来(移除后重新添加的)，而list hash的生命周期还是继承以前的
         * 
         */
        public static readonly RedisUtil Instance = new RedisUtil();
        private RedisUtil() { }



        #region string

        public T Get<T>(string key)
        {
            try
            {
                using (var redis = RedisManager.GetClient())
                {
                    return redis.Get<T>(key);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Get<T>() =>> " + ex.Message);
            }
        }


        public bool Set<T>(string key, T value, DateTime? datetime = null)
        {
            try
            {
                using (var redis = RedisManager.GetClient())
                {
                    if (datetime != null) return redis.Set<T>(key, value, datetime.Value);
                    return redis.Set<T>(key, value);
                }
            }
            catch (Exception ex) { throw new Exception("Set<T>() =>> " + ex.Message); }
        }

        #endregion


        #region List
        /// <summary>
        /// 获取key下的list全部
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<string> GetListAll(string key)
        {
            try
            {
                using (var redis = RedisManager.GetClient())
                {
                    return redis.GetAllItemsFromList(key);
                }
            }
            catch (Exception ex) { throw new Exception("GetListAll() =>> " + ex.Message); }
        }


        /// <summary>
        /// 批量添加list
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="datetime"></param>
        public void AddListRange(string key, List<string> value, DateTime? datetime = null)
        {
            try
            {
                using (var redis = RedisManager.GetClient())
                {
                    redis.AddRangeToList(key, value);
                    if (datetime != null)
                        redis.ExpireEntryAt(key, datetime.Value);
                }
            }
            catch (Exception ex) { throw new Exception("AddListRange() =>> " + ex.Message); }
        }


        /// <summary>
        /// 向list末尾位置插入值
        /// </summary>
        /// <param name="key">listid,数组key</param>
        /// <param name="value">本次插入的值</param>
        public void PushItemToList(string key, string value, DateTime? datetime = null)
        {
            try
            {
                using (var redis = RedisManager.GetClient())
                {
                    redis.PushItemToList(key, value);
                    if (datetime != null)
                        redis.ExpireEntryAt(key, datetime.Value);
                }
            }
            catch (Exception ex) { throw new Exception("PushItemToList() =>> " + ex.Message); }
        }
        /// <summary>
        /// 向list初始位置插入值
        /// </summary>
        /// <param name="key">listid,数组key</param>
        /// <param name="value">本次插入的值</param>
        public void PrependItemToList(string key, string value, DateTime? datetime = null)
        {
            try
            {
                using (var redis = RedisManager.GetClient())
                {
                    redis.PrependItemToList(key, value);
                    if (datetime != null)
                        redis.ExpireEntryAt(key, datetime.Value);
                }
            }
            catch (Exception ex) { throw new Exception("PrependItemToList() =>> " + ex.Message); }
        }

        /// <summary>
        /// 根据key清空list ！！只有一条记录的时候无法删除。。 bug
        /// </summary>
        /// <param name="key"></param>
        public void ClearList(string key)
        {
            try
            {
                using (var redis = RedisManager.GetClient())
                {
                    redis.RemoveAllFromList(key);
                }
            }
            catch (Exception ex) { throw new Exception("ClearList() =>> " + ex.Message); }
        }

        /// <summary>
        /// 移除list中某项值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value">要移除的项的值</param>
        public void RemoveListItem(string key, string value)
        {
            try
            {
                using (var redis = RedisManager.GetClient())
                {
                    long r = redis.RemoveItemFromList(key, value);
                }
            }
            catch (Exception ex) { throw new Exception("RemoveListItem() =>> " + ex.Message); }
        }

        #endregion




        #region Hash


        /// <summary>
        /// Hash设置值 key存在则覆盖
        /// </summary>
        /// <param name="hashid"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool HashSet(string hashid, string key, string value, DateTime? datetime = null)
        {
            try
            {
                using (var redis = RedisManager.GetClient())
                {

                    if (redis.SetEntryInHash(hashid, key, value))
                    {
                        if (datetime != null)
                            redis.ExpireEntryAt(hashid, datetime.Value);
                        return true;
                    }
                    return false;
                    //return redis.HSet(hashid, key.ToUtf8Bytes(), value.ToUtf8Bytes());
                }
            }
            catch (Exception ex) { throw new Exception("HashSet() =>> " + ex.Message); }
        }

        /// <summary>
        /// 批量添加hash
        /// </summary>
        /// <param name="hashid"></param>
        /// <param name="key"></param>
        /// <param name="KeyValuePair"></param>
        public void AddHashRange(string hashid, IEnumerable<KeyValuePair<string, string>> KeyValuePair, DateTime? datetime = null)
        {
            try
            {
                using (var redis = RedisManager.GetClient())
                {
                    redis.SetRangeInHash(hashid, KeyValuePair);
                    if (datetime != null)
                        redis.ExpireEntryAt(hashid, datetime.Value);
                }
            }
            catch (Exception ex) { throw new Exception("AddHashRange() =>> " + ex.Message); }
        }



        /// <summary>
        /// 获取单个value 根据hashid与key值
        /// </summary>
        /// <param name="hashid"></param>
        /// <param name="key"></param>
        /// <returns></returns>

        public string GetHashValue(string hashid, string key)
        {
            try
            {
                using (var redis = RedisManager.GetClient())
                {
                    return redis.GetValueFromHash(hashid, key);
                }
            }
            catch (Exception ex) { throw new Exception("GetHashValue() =>> " + ex.Message); }
        }
        /// <summary>
        /// 根据hashid与key获取多个值（支持多个key）
        /// </summary>
        /// <param name="hashid"></param>
        /// <param name="keys"></param>
        /// <returns></returns>
        public List<string> GetValuesFromHash(string hashid, string[] keys)
        {
            try
            {
                using (var redis = RedisManager.GetClient())
                {
                    return redis.GetValuesFromHash(hashid, keys);
                }
            }
            catch (Exception ex) { throw new Exception("GetValuesFromHash() =>> " + ex.Message); }
        }

        /// <summary>
        /// 获取hashid下的【所有key】
        /// </summary>
        /// <param name="hashid"></param>
        /// <returns></returns>
        public List<string> GetHashKeys(string hashid)
        {
            try
            {
                using (var redis = RedisManager.GetClient())
                {
                    return redis.GetHashKeys(hashid);
                }
            }
            catch (Exception ex) { throw new Exception("GetHashKeys() =>> " + ex.Message); }
        }

        /// <summary>
        /// 根据hashid获取【所有value】
        /// </summary>
        /// <param name="hashid"></param>
        /// <returns></returns>
        public List<string> GetHashValues(string hashid)
        {
            try
            {
                using (var redis = RedisManager.GetClient())
                {
                    return redis.GetHashValues(hashid);
                }
            }
            catch (Exception ex) { throw new Exception("GetHashValues() =>> " + ex.Message); }
        }

        /// <summary>
        /// 根据hashid获取【所有value】
        /// </summary>
        /// <param name="hashid"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetHashAll(string hashid)
        {
            try
            {
                using (var redis = RedisManager.GetClient())
                {
                    byte[][] bbyte = redis.HGetAll(hashid);
                    Dictionary<string, string> dic = new Dictionary<string, string>();
                    for (int i = 0; i < bbyte.Length; i += 2)
                    {
                        string key = bbyte[i].FromUtf8Bytes();
                        string value = bbyte[i + 1].FromUtf8Bytes();
                        dic.Add(key, value);
                    }
                    return dic;

                }
            }
            catch (Exception ex) { throw new Exception("GetHashAll() =>> " + ex.Message); }
        }

        /// <summary>
        /// 获取hashid下的所有数量
        /// </summary>
        /// <param name="hashid"></param>
        /// <returns></returns>
        public int GetHashCount(string hashid)
        {
            try
            {
                using (var redis = RedisManager.GetClient())
                {
                    return (int)redis.GetHashCount(hashid);
                }
            }
            catch (Exception ex) { throw new Exception("GetHashCount() =>> " + ex.Message); }
        }

        /// <summary>
        /// 判断key是否存在于hash中
        /// </summary>
        /// <param name="hashid"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool HashContainsKey(string hashid, string key)
        {
            try
            {
                using (var redis = RedisManager.GetClient())
                {
                    return redis.HashContainsEntry(hashid, key);
                }
            }
            catch (Exception ex) { throw new Exception("HashContainsKey() =>> " + ex.Message); }
        }


        /// <summary>
        /// 将一个对象存入hash
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        public void HashSetEntiy<T>(T entity)
        {
            try
            {
                using (var redis = RedisManager.GetClient())
                {
                    redis.StoreAsHash(entity);   //存了之后要根据id去获取，有没返回id？？ GetFromHash（）
                }
            }
            catch (Exception ex) { throw new Exception("HashSetEntiy() =>> " + ex.Message); }
        }

        /// <summary>
        /// 根据hashid的key移除
        /// </summary>
        /// <param name="hashid"></param>
        /// <param name="key"></param>
        public bool RemoveHashItem(string hashid, string key)
        {
            try
            {
                using (var redis = RedisManager.GetClient())
                {
                    return redis.RemoveEntryFromHash(hashid, key);
                }
            }
            catch (Exception ex) { throw new Exception("RemoveHashItem() =>> " + ex.Message); }
        }


        #endregion

    }
}
