class LlmPrice < ApplicationRecord
  attribute :id, default: -> { Cuid.generate }

  validates :usage_type, presence: true, uniqueness: { scope: %i[model_id pricing_tier_id] }
  validates :price, presence: true

  belongs_to :model, class_name: "LlmModel", inverse_of: :prices
  belongs_to :pricing_tier, class_name: "LlmPricingTier", inverse_of: :prices
end
