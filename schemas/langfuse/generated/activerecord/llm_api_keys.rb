class LlmApiKeys < ApplicationRecord
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }
  attribute :custom_models, default: -> { [] }
  attribute :with_default_models, default: true
  attribute :extra_header_keys, default: -> { [] }

  validates :provider, presence: true, uniqueness: { scope: :project_id, conditions: -> { where.not(project_id: nil) } }
  validates :provider, uniqueness: { scope: :organization_id, conditions: -> { where.not(organization_id: nil) } }
  validates :adapter, presence: true
  validates :display_secret_key, presence: true
  validates :secret_key, presence: true

  belongs_to :project, optional: true, inverse_of: :llm_api_keys
  belongs_to :organization, optional: true, inverse_of: :organization_llm_api_keys
  has_many :default_llm_model, class_name: "DefaultLlmModel", foreign_key: "llm_api_key_id", inverse_of: :llm_api_key,
                               dependent: :destroy
  has_many :topic_summary_configs, class_name: "TopicConfig", foreign_key: "summary_llm_api_key_id",
                                   inverse_of: :summary_llm_api_key, dependent: :nullify
  has_many :topic_embedding_configs, class_name: "TopicConfig", foreign_key: "embedding_llm_api_key_id",
                                     inverse_of: :embedding_llm_api_key, dependent: :nullify
  has_many :topic_clustering_configs, class_name: "TopicConfig", foreign_key: "clustering_llm_api_key_id",
                                      inverse_of: :clustering_llm_api_key, dependent: :nullify
end
