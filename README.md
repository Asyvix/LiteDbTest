# LiteDbTest
그냥 테스트

3700x
64gb ram(ddr4 3000mhz)
pm981a 기준
9.81ms, 2gb ram이 필요함.
당연히 정확하지도 않고 대충한데다 디버그 기준.

서비스에 사용하기 위해서는 캐싱이 필요할것으로 보임

서비스에 데이터베이스를 공유할경우 고유 키를 사용하게되면 고유 키의 증가에 대해서 서비스의 사용량을 측정하거나 예측할 수 없는 무언가의 정보를 추론할 가능성이 있음.
그렇다고 키를 격리하거나 guid등은 귀찮거나 비효율적임
따라서 Database Per Acccount 개념을 적용하고싶음.