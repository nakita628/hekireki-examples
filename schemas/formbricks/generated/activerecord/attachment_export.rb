class AttachmentExport < ApplicationRecord
  self.table_name = "AttachmentExport"
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid2.call }
  attribute :dispatchCount, default: 0
  attribute :truncated, default: false

  enum :status, { queued: "queued", running: "running", completed: "completed", failed: "failed", expired: "expired" },
       default: :queued, validate: true

  validates :timeZone, presence: true
  validates :fileCount, presence: true
  validates :responseCount, presence: true

  belongs_to :survey, foreign_key: [ :surveyId, :workspaceId ], primary_key: [ :id, :workspaceId ],
                      inverse_of: :attachment_exports
  belongs_to :workspace, foreign_key: "workspaceId", inverse_of: :attachment_exports
  belongs_to :requested_by, class_name: "User", foreign_key: "requestedById", optional: true,
                            inverse_of: :attachment_exports
end
