class PricingTier < ApplicationRecord
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }
  attribute :is_default, default: false

  validates :name, presence: true, uniqueness: { scope: :model_id }
  validates :priority, presence: true, uniqueness: { scope: :model_id }
  validates :conditions, exclusion: { in: [ nil ] }

  belongs_to :model, inverse_of: :pricing_tiers
  has_many :prices, dependent: :destroy
end
