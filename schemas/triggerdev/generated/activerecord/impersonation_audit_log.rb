class ImpersonationAuditLog < ApplicationRecord
  self.table_name = "ImpersonationAuditLog"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt

  enum :action, { start: "START", stop: "STOP" }, validate: true

  belongs_to :admin, class_name: "User", foreign_key: "adminId", inverse_of: :impersonations_performed
  belongs_to :target, class_name: "User", foreign_key: "targetId", inverse_of: :impersonations_received
end
