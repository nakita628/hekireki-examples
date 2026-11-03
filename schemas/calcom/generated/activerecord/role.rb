class Role < ApplicationRecord
  self.table_name = "Role"
  self.inheritance_column = nil
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :type, { system: "SYSTEM", custom: "CUSTOM" }, default: :custom, prefix: true, validate: true

  validates :name, presence: true, uniqueness: { scope: :teamId, conditions: -> { where.not(teamId: nil) } }

  belongs_to :team, foreign_key: "teamId", optional: true, inverse_of: :roles
  has_many :permissions, class_name: "RolePermission", foreign_key: "roleId", inverse_of: :role, dependent: :destroy
  has_many :memberships, foreign_key: "customRoleId", inverse_of: :custom_role, dependent: :nullify
end
