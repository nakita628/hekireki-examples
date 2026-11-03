class Annotation < ApplicationRecord
  self.table_name = "annotation"
  self.primary_key = "annotation_id"

  attribute :all_day, default: true

  validates :date, presence: true
  validates :note, presence: true, length: { maximum: 500 }

  belongs_to :website, inverse_of: :annotations
end
