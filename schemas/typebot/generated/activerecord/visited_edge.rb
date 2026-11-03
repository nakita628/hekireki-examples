class VisitedEdge < ApplicationRecord
  self.table_name = "VisitedEdge"

  validates :edgeId, presence: true
  validates :index, presence: true, uniqueness: { scope: :resultId }

  belongs_to :result, foreign_key: "resultId", inverse_of: :edges
end
