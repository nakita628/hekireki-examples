class EvaluatorVersion < ApplicationRecord
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }
  attribute :vars, default: -> { [] }

  enum :source_code_language, { python: "PYTHON", typescript: "TYPESCRIPT" }, validate: { allow_nil: true }

  validates :version, presence: true, uniqueness: { scope: :evaluator_id }
  validates :source_code, length: { maximum: 262144 }

  belongs_to :evaluator, inverse_of: :versions
  belongs_to :created_by_user, class_name: "User", optional: true, inverse_of: :evaluator_versions_created
  has_many :topic_clustering_runs, foreign_key: %i[facet_id facet_version], primary_key: %i[evaluator_id version],
                                   inverse_of: :facet, dependent: :destroy
end
