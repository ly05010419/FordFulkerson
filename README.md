# 目的：求两点之间的最大流


## fordFulkerson
> 1. 首先使用Dijkstra 或者 MooreBellmanFord求两点之间的最小路径（增广路径）
> 2. 创建ResidualGraph（残留网络）
> 3. 继续在残留网络中使用Dijkstra 或者 MooreBellmanFord求两点之间的最小路径（增广路径）
> 4. 直到没有增广路径为止
> 5. 把所有的增广路径的容量加起来就是最大流



https://www.youtube.com/watch?v=Z8gcjuS0Vb8
http://www.cnblogs.com/gaochundong/p/ford_fulkerson_maximum_flow_algorithm.html
