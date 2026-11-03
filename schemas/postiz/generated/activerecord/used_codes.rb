class UsedCodes < ApplicationRecord
  self.table_name = "UsedCodes"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { SecureRandom.uuid }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :code, presence: true

  belongs_to :organization, foreign_key: "orgId", inverse_of: :used_codes
end
