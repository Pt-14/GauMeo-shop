-- Insert Service Notes (Lưu Ý Quan Trọng)

-- Common Notes for all services
INSERT INTO ServiceNotes (Title, Content, Icon, NoteType, IsActive, DisplayOrder, CreatedAt, ServiceId)
VALUES 
-- Service 1: Pet Spa & Grooming Notes
(N'Tiêm phòng', N'Thú cưng cần được tiêm phòng đầy đủ trước ít nhất 2 tuần khi sử dụng dịch vụ. Vui lòng mang theo sổ tiêm phòng.', '⚠️', 'warning', 1, 1, GETDATE(), 1),
(N'Thú cưng già và bệnh lý', N'Đối với thú cưng trên 7 tuổi hoặc có bệnh lý tim mạch, cần tư vấn bác sĩ thú y trước khi sử dụng dịch vụ.', '⚠️', 'warning', 1, 2, GETDATE(), 1),
(N'Đặt lịch', N'Nên đặt lịch trước 1-2 ngày để đảm bảo có chỗ. Dịch vụ có thể từ chối nếu thú cưng có dấu hiệu bất thường.', 'ℹ️', 'info', 1, 3, GETDATE(), 1),

-- Service 2: Pet Hotel Notes
(N'Tiêm phòng', N'Thú cưng cần được tiêm phòng đầy đủ trước ít nhất 2 tuần khi sử dụng dịch vụ. Vui lòng mang theo sổ tiêm phòng.', '⚠️', 'warning', 1, 1, GETDATE(), 2),
(N'Thú cưng già và bệnh lý', N'Đối với thú cưng trên 7 tuổi hoặc có bệnh lý tim mạch, cần tư vấn bác sĩ thú y trước khi sử dụng dịch vụ.', '⚠️', 'warning', 1, 2, GETDATE(), 2),
(N'Đặt lịch', N'Nên đặt lịch trước 1-2 ngày để đảm bảo có chỗ. Dịch vụ có thể từ chối nếu thú cưng có dấu hiệu bất thường.', 'ℹ️', 'info', 1, 3, GETDATE(), 2),

-- Service 3: Pet Swimming Notes (Special notes for swimming)
(N'Dịch vụ bơi lội', N'Chó bị nấm da, viêm da hoặc có vết thương hở không được sử dụng dịch vụ bể bơi để tránh lây nhiễm và làm nặng tình trạng bệnh.', '⚠️', 'warning', 1, 1, GETDATE(), 3),
(N'Chỉ dành cho chó', N'Dịch vụ này chỉ phục vụ chó vì mèo thường sợ nước và có thể bị stress nghiêm trọng.', '⚠️', 'warning', 1, 2, GETDATE(), 3),
(N'Tiêm phòng', N'Thú cưng cần được tiêm phòng đầy đủ trước ít nhất 2 tuần khi sử dụng dịch vụ. Vui lòng mang theo sổ tiêm phòng.', '⚠️', 'warning', 1, 3, GETDATE(), 3),
(N'Thú cưng già và bệnh lý', N'Đối với thú cưng trên 7 tuổi hoặc có bệnh lý tim mạch, cần tư vấn bác sĩ thú y trước khi sử dụng dịch vụ.', '⚠️', 'warning', 1, 4, GETDATE(), 3),
(N'Đặt lịch', N'Nên đặt lịch trước 1-2 ngày để đảm bảo có chỗ. Dịch vụ có thể từ chối nếu thú cưng có dấu hiệu bất thường.', 'ℹ️', 'info', 1, 5, GETDATE(), 3),

-- Service 4: Pet Daycare Notes
(N'Tiêm phòng', N'Thú cưng cần được tiêm phòng đầy đủ trước ít nhất 2 tuần khi sử dụng dịch vụ. Vui lòng mang theo sổ tiêm phòng.', '⚠️', 'warning', 1, 1, GETDATE(), 4),
(N'Thú cưng già và bệnh lý', N'Đối với thú cưng trên 7 tuổi hoặc có bệnh lý tim mạch, cần tư vấn bác sĩ thú y trước khi sử dụng dịch vụ.', '⚠️', 'warning', 1, 2, GETDATE(), 4),
(N'Đặt lịch', N'Nên đặt lịch trước 1-2 ngày để đảm bảo có chỗ. Dịch vụ có thể từ chối nếu thú cưng có dấu hiệu bất thường.', 'ℹ️', 'info', 1, 3, GETDATE(), 4),

-- Service 5: Pet Training Notes
(N'Tiêm phòng', N'Thú cưng cần được tiêm phòng đầy đủ trước ít nhất 2 tuần khi sử dụng dịch vụ. Vui lòng mang theo sổ tiêm phòng.', '⚠️', 'warning', 1, 1, GETDATE(), 5),
(N'Thú cưng già và bệnh lý', N'Đối với thú cưng trên 7 tuổi hoặc có bệnh lý tim mạch, cần tư vấn bác sĩ thú y trước khi sử dụng dịch vụ.', '⚠️', 'warning', 1, 2, GETDATE(), 5),
(N'Đặt lịch', N'Nên đặt lịch trước 1-2 ngày để đảm bảo có chỗ. Dịch vụ có thể từ chối nếu thú cưng có dấu hiệu bất thường.', 'ℹ️', 'info', 1, 3, GETDATE(), 5); 