class DefaultLlmModel < ApplicationRecord
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }

  validates :project_id, uniqueness: true
  validates :project_id, uniqueness: true
  validates :provider, presence: true
  validates :adapter, presence: true
  validates :model, presence: true

  belongs_to :project, inverse_of: :default_llm_model
  belongs_to :llm_api_key, class_name: "LlmApiKeys", inverse_of: :default_llm_model
end
