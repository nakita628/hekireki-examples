class TopicClusteringRun < ApplicationRecord
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }
  attribute :status, default: "pending"
  attribute :config, default: -> { JSON.parse("{}") }
  attribute :topic_version_ids, default: -> { [] }

  belongs_to :project, inverse_of: :topic_clustering_runs
  belongs_to :facet, class_name: "EvaluatorVersion", foreign_key: %i[facet_id facet_version],
                     primary_key: %i[evaluator_id version], inverse_of: :topic_clustering_runs
end
