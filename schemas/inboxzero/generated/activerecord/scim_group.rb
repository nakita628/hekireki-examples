class ScimGroup < ApplicationRecord
  self.table_name = "scimGroup"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :revision, default: 0
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :connectionId, presence: true
  validates :provisioningDomainId, presence: true
  validates :displayName, presence: true
  validates :displayNameKey, presence: true, uniqueness: true
  validates :displayNameKey, uniqueness: true
  validates :externalIdKey, uniqueness: { allow_nil: true }
  validates :externalIdKey, uniqueness: { allow_nil: true }
  validates :orderKey, presence: true, uniqueness: true
  validates :orderKey, uniqueness: true

  has_many :scim_group_members, foreign_key: "groupId", inverse_of: :scim_group, dependent: :destroy
end
