class LiteLLMShadowEvalJob < ApplicationRecord
  self.table_name = "LiteLLM_ShadowEvalJob"
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }
  attribute :target_type, default: "key"
  attribute :router_names, default: -> { [] }
  attribute :models, default: -> { [] }
  attribute :direction, default: "forward"

  validates :group_id, presence: true
  validates :target_id, presence: true
  validates :router_name, presence: true
  validates :judge_model, presence: true
  validates :shadow_percentage, presence: true
  validates :max_turns, presence: true
  validates :ends_at, presence: true
end
