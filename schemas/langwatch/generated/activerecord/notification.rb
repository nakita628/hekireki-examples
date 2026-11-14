class Notification < ApplicationRecord
  self.table_name = "Notification"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Nanoid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :metadata, exclusion: { in: [ nil ] }
  validates :sentAt, presence: true

  belongs_to :organization, foreign_key: "organizationId", optional: true, inverse_of: :notifications
  belongs_to :project, foreign_key: "projectId", optional: true, inverse_of: :notifications
end
