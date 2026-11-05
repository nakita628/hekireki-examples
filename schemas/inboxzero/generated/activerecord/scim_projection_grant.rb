class ScimProjectionGrant < ApplicationRecord
  self.table_name = "scimProjectionGrant"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :connectionId, presence: true
  validates :provisioningDomainId, presence: true
  validates :sourceKind, presence: true
  validates :sourceId, presence: true
  validates :role, presence: true
  validates :grantKey, presence: true, uniqueness: true
  validates :grantKey, uniqueness: true

  belongs_to :scim_user, foreign_key: "scimUserId", inverse_of: :scim_projection_grants
  belongs_to :user, foreign_key: "userId", inverse_of: :scim_projection_grants
end
