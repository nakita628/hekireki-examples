class AttachmentsTable < ApplicationRecord
  self.table_name = "attachments_table"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :updated_at, :last_modified_time

  validates :attachment_id, presence: true
  validates :name, presence: true
  validates :token, presence: true
  validates :table_id, presence: true
  validates :record_id, presence: true
  validates :field_id, presence: true
  validates :created_by, presence: true
end
