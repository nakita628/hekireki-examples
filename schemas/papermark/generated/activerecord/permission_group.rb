class PermissionGroup < ApplicationRecord
  self.table_name = "PermissionGroup"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :name, presence: true

  belongs_to :dataroom, foreign_key: "dataroomId", inverse_of: :permission_groups
  belongs_to :team, foreign_key: "teamId", inverse_of: :permission_groups
  has_many :links, foreign_key: "permissionGroupId", inverse_of: :permission_group, dependent: :nullify
  has_many :access_controls, class_name: "PermissionGroupAccessControls", foreign_key: "groupId", inverse_of: :group,
                             dependent: :destroy
end
