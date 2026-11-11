class LiteLLMModelTable < ApplicationRecord
  self.table_name = "LiteLLM_ModelTable"

  validates :created_by, presence: true
  validates :updated_by, presence: true

  has_one :team, class_name: "LiteLLMTeamTable", foreign_key: "model_id", inverse_of: :litellm_model_table,
                 dependent: :nullify
end
