class LlmPricingTier < ApplicationRecord
  attribute :id, default: -> { Cuid.generate }
  attribute :is_default, default: true
  attribute :priority, default: 0
  attribute :conditions, default: -> { JSON.parse("[]") }

  validates :name, presence: true, uniqueness: { scope: :model_id }
  validates :priority, uniqueness: { scope: :model_id }

  belongs_to :model, class_name: "LlmModel", inverse_of: :pricing_tiers
  has_many :prices, class_name: "LlmPrice", foreign_key: "pricing_tier_id", inverse_of: :pricing_tier,
                    dependent: :destroy
end
