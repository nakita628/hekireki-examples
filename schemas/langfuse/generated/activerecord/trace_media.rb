class TraceMedia < ApplicationRecord
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }

  enum :origin, { unknown: "UNKNOWN", client_upload: "CLIENT_UPLOAD", ingestion_media_extraction: "INGESTION_MEDIA_EXTRACTION", ingestion_field_overflow: "INGESTION_FIELD_OVERFLOW" },
       default: :unknown, validate: true

  validates :project_id, presence: true, uniqueness: { scope: %i[trace_id media_id field] }
  validates :media_id, presence: true
  validates :trace_id, presence: true
  validates :field, presence: true
end
