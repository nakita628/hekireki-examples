class AssetProfileSplit < ApplicationRecord
  self.table_name = "AssetProfileSplit"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { SecureRandom.uuid }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :date, presence: true, uniqueness: { scope: :symbolProfileId }
  validates :denominator, presence: true
  validates :numerator, presence: true

  belongs_to :symbol_profile, foreign_key: "symbolProfileId", inverse_of: :asset_profile_splits
end
