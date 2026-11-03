class GitHub < ApplicationRecord
  self.table_name = "GitHub"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { SecureRandom.uuid }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :token, presence: true

  belongs_to :organization, foreign_key: "organizationId", inverse_of: :github
end
