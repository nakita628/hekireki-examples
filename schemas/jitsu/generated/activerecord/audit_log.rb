class AuditLog < ApplicationRecord
  self.table_name = "AuditLog"
  self.inheritance_column = nil

  attribute :id, default: -> { Cuid.generate }
  attribute :severity, default: "info"

  validates :type, presence: true
end
