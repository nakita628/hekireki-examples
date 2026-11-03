class Customer < ApplicationRecord
  self.table_name = "Customer"

  attribute :sales, default: 0
  attribute :saleAmount, default: 0
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :externalId, uniqueness: { scope: :projectId, allow_nil: true }
  validates :stripeCustomerId, uniqueness: { allow_nil: true }

  belongs_to :project, foreign_key: "projectId", inverse_of: :customers
  belongs_to :link, foreign_key: "linkId", optional: true, inverse_of: :customers
end
