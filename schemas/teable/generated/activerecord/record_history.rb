class RecordHistory < ApplicationRecord
  self.table_name = "record_history"

  attribute :id, default: -> { Cuid.generate }

  validates :table_id, presence: true
  validates :record_id, presence: true
  validates :field_id, presence: true
  validates :before, presence: true
  validates :after, presence: true
  validates :created_by, presence: true
end
