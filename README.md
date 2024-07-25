- Migration: là một tính năng của EF dùng để tạo bảng data trực tiếp mà không cần phải vào sql.

- Enum:  sử dụng để đại diện cho một tập hợp các giá trị hằng số có tên.

- IEnumerable: là một interface dùng để chạy qua danh sách giống như List.

- Repository: là một thiết kế giúp quản lý dữ liệu một cách hiệu quả và sạch sẽ.
		Mẫu này tách biệt logic truy cập dữ liệu từ logic nghiệp vụ của ứng dụng, giúp mã nguồn dễ bảo trì và mở rộng hơn.

		Các Thành Phần Của Repository Pattern
			Repository Interface: Định nghĩa các phương thức cơ bản để truy cập và quản lý dữ liệu.
			Repository Class: Cài đặt các phương thức được định nghĩa trong interface, thực hiện các thao tác truy cập dữ liệu thực tế.
			DbContext: Đối tượng của Entity Framework Core để tương tác với cơ sở dữ liệu.