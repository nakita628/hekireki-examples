class CustomRole < ApplicationRecord
  self.table_name = "CustomRole"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Nanoid.generate }
  attribute :kind, default: "custom"
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :name, presence: true, uniqueness: { scope: :organizationId }
  validates :permissions, exclusion: { in: [ nil ] }

  belongs_to :organization, foreign_key: "organizationId", inverse_of: :custom_roles
  has_many :assigned_users, class_name: "TeamUser", foreign_key: "assignedRoleId", inverse_of: :assigned_role,
                            dependent: :nullify
  has_many :role_bindings, foreign_key: "customRoleId", inverse_of: :custom_role, dependent: :nullify
end
