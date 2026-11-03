class LlmModel < ApplicationRecord
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }
  attribute :source, default: "default"
  attribute :capabilities, default: -> { [] }
  attribute :is_hidden, default: false

  validates :friendly_id, presence: true, uniqueness: true
  validates :model_name, presence: true,
                         uniqueness: { scope: %i[project_id start_date], conditions: -> { where.not(project_id: nil).where.not(start_date: nil) } }
  validates :match_pattern, presence: true

  belongs_to :project, optional: true, inverse_of: :llm_models
  has_many :pricing_tiers, class_name: "LlmPricingTier", foreign_key: "model_id", inverse_of: :model,
                           dependent: :destroy
  has_many :prices, class_name: "LlmPrice", foreign_key: "model_id", inverse_of: :model, dependent: :destroy
end
