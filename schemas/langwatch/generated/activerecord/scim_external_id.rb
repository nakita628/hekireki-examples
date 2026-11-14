class ScimExternalId < ApplicationRecord
  self.table_name = "ScimExternalId"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Nanoid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :organizationId, presence: true
  validates :connectionId, presence: true, uniqueness: { scope: :externalId }
  validates :externalId, presence: true

  belongs_to :user, foreign_key: "userId", inverse_of: :scim_external_ids
end
