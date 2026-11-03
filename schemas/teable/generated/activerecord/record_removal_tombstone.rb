class RecordRemovalTombstone < ApplicationRecord
  self.table_name = "record_removal_tombstone"
  self.inheritance_column = nil

  validates :table_id, presence: true
  validates :record_id, presence: true
  validates :type, presence: true
end
