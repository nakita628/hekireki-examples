class ScimToken < ApplicationRecord
  self.table_name = "ScimToken"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Nanoid.generate }
  attribute :hashScheme, default: "sha256"
  alias_attribute :created_at, :createdAt

  validates :hashedToken, presence: true, uniqueness: true

  belongs_to :organization, foreign_key: "organizationId", inverse_of: :scim_tokens
end
