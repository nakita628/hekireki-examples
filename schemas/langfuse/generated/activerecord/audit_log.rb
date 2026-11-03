class AuditLog < ApplicationRecord
  self.inheritance_column = nil
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }

  enum :type, { user: "USER", api_key: "API_KEY" }, default: :user, validate: true

  validates :org_id, presence: true
  validates :resource_type, presence: true
  validates :resource_id, presence: true
  validates :action, presence: true
end
