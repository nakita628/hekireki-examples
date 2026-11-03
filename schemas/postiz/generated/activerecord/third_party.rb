class ThirdParty < ApplicationRecord
  self.table_name = "ThirdParty"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { SecureRandom.uuid }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :identifier, presence: true
  validates :name, presence: true
  validates :internalId, presence: true, uniqueness: { scope: :organizationId }
  validates :apiKey, presence: true

  belongs_to :organization, foreign_key: "organizationId", inverse_of: :third_party
end
