class TraceEditOverlay < ApplicationRecord
  self.table_name = "TraceEditOverlay"

  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :traceId, presence: true, uniqueness: { scope: :projectId }
  validates :patch, exclusion: { in: [ nil ] }

  belongs_to :project, foreign_key: "projectId", inverse_of: :trace_edit_overlays
  belongs_to :created_by, class_name: "User", foreign_key: "createdById", optional: true,
                          inverse_of: :trace_edit_overlays_created
  belongs_to :updated_by, class_name: "User", foreign_key: "updatedById", optional: true,
                          inverse_of: :trace_edit_overlays_updated
end
