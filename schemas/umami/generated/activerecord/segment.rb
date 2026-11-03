class Segment < ApplicationRecord
  self.table_name = "segment"
  self.primary_key = "segment_id"
  self.inheritance_column = nil

  validates :type, presence: true, length: { maximum: 50 }
  validates :name, presence: true, length: { maximum: 200 }
  validates :parameters, exclusion: { in: [ nil ] }

  belongs_to :website, inverse_of: :segments
end
