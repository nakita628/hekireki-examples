class TopicConfig < ApplicationRecord
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }
  attribute :enabled, default: false
  attribute :embedding_dimensions, default: 1024

  enum :block_reason, { llm_connection_auth_invalid: "LLM_CONNECTION_AUTH_INVALID", llm_connection_billing_exhausted: "LLM_CONNECTION_BILLING_EXHAUSTED", llm_connection_endpoint_unreachable: "LLM_CONNECTION_ENDPOINT_UNREACHABLE", llm_connection_missing: "LLM_CONNECTION_MISSING", default_eval_model_missing: "DEFAULT_EVAL_MODEL_MISSING", eval_model_config_invalid: "EVAL_MODEL_CONFIG_INVALID", eval_model_unavailable: "EVAL_MODEL_UNAVAILABLE", provider_account_not_ready: "PROVIDER_ACCOUNT_NOT_READY" },
       validate: { allow_nil: true }

  validates :project_id, uniqueness: true

  belongs_to :created_by_user, class_name: "User", foreign_key: "created_by", optional: true,
                               inverse_of: :topic_configs_created
  belongs_to :updated_by_user, class_name: "User", foreign_key: "updated_by", optional: true,
                               inverse_of: :topic_configs_updated
  belongs_to :project
  belongs_to :summary_llm_api_key, class_name: "LlmApiKeys", optional: true, inverse_of: :topic_summary_configs
  belongs_to :embedding_llm_api_key, class_name: "LlmApiKeys", optional: true, inverse_of: :topic_embedding_configs
  belongs_to :clustering_llm_api_key, class_name: "LlmApiKeys", optional: true, inverse_of: :topic_clustering_configs
end
