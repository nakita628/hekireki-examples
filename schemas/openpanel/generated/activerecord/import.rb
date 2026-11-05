class Import < ApplicationRecord
  attribute :totalEvents, default: 0
  attribute :processedEvents, default: 0
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :status, { pending: "pending", processing: "processing", completed: "completed", failed: "failed" },
       validate: true

  validates :config, exclusion: { in: [ nil ] }

  belongs_to :project, foreign_key: "projectId", inverse_of: :imports
end
