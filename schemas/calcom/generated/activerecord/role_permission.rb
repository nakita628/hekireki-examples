class RolePermission < ApplicationRecord
  self.table_name = "RolePermission"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { SecureRandom.uuid }
  alias_attribute :created_at, :createdAt

  validates :resource, presence: true, uniqueness: { scope: [ :roleId, :action ] }
  validates :action, presence: true

  belongs_to :role, foreign_key: "roleId", inverse_of: :permissions
end
