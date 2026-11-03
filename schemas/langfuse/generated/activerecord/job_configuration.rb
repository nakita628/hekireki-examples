class JobConfiguration < ApplicationRecord
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }
  attribute :time_scope, default: -> { %w[NEW] }

  enum :job_type, { eval: "EVAL" }, prefix: true, validate: true
  enum :status, { active: "ACTIVE", inactive: "INACTIVE" }, default: :active, validate: true
  enum :block_reason, { llm_connection_auth_invalid: "LLM_CONNECTION_AUTH_INVALID", llm_connection_billing_exhausted: "LLM_CONNECTION_BILLING_EXHAUSTED", llm_connection_endpoint_unreachable: "LLM_CONNECTION_ENDPOINT_UNREACHABLE", llm_connection_missing: "LLM_CONNECTION_MISSING", default_eval_model_missing: "DEFAULT_EVAL_MODEL_MISSING", eval_model_config_invalid: "EVAL_MODEL_CONFIG_INVALID", eval_model_unavailable: "EVAL_MODEL_UNAVAILABLE", provider_account_not_ready: "PROVIDER_ACCOUNT_NOT_READY" },
       validate: { allow_nil: true }

  validates :score_name, presence: true
  validates :filter, exclusion: { in: [ nil ] }
  validates :target_object, presence: true
  validates :variable_mapping, exclusion: { in: [ nil ] }
  validates :sampling, presence: true
  validates :delay, presence: true

  belongs_to :project, inverse_of: :job_configuration
  belongs_to :eval_template, optional: true, inverse_of: :job_configuration
end
