class Evaluator < ApplicationRecord
  self.inheritance_column = nil
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }
  attribute :is_built_in, default: false

  enum :type, { llm_as_judge: "LLM_AS_JUDGE", code: "CODE", decision_model: "DECISION_MODEL", facet: "FACET" },
       validate: true
  enum :block_reason, { llm_connection_auth_invalid: "LLM_CONNECTION_AUTH_INVALID", llm_connection_billing_exhausted: "LLM_CONNECTION_BILLING_EXHAUSTED", llm_connection_endpoint_unreachable: "LLM_CONNECTION_ENDPOINT_UNREACHABLE", llm_connection_missing: "LLM_CONNECTION_MISSING", default_eval_model_missing: "DEFAULT_EVAL_MODEL_MISSING", eval_model_config_invalid: "EVAL_MODEL_CONFIG_INVALID", eval_model_unavailable: "EVAL_MODEL_UNAVAILABLE", provider_account_not_ready: "PROVIDER_ACCOUNT_NOT_READY" },
       validate: { allow_nil: true }

  validates :name, presence: true

  belongs_to :project, inverse_of: :evaluator
  belongs_to :created_by_user, class_name: "User", optional: true, inverse_of: :evaluators_created
  has_many :versions, class_name: "EvaluatorVersion", inverse_of: :evaluator, dependent: :destroy
  has_many :assignments, class_name: "EvaluationRuleEvaluatorAssignment", inverse_of: :evaluator, dependent: :destroy
end
