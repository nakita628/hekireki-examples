class Price < ApplicationRecord
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }

  validates :usage_type, presence: true, uniqueness: { scope: %i[model_id pricing_tier_id] }
  validates :price, presence: true

  belongs_to :model, inverse_of: :price
  belongs_to :project, optional: true, inverse_of: :price
  belongs_to :pricing_tier, inverse_of: :prices
end
