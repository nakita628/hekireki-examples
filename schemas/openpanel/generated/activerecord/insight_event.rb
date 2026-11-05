class InsightEvent < ApplicationRecord
  alias_attribute :created_at, :createdAt

  validates :eventKind, presence: true

  belongs_to :insight, class_name: "ProjectInsight", foreign_key: "insightId", inverse_of: :events
end
