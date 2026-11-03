class Report < ApplicationRecord
  self.table_name = "report"
  self.primary_key = "report_id"
  self.inheritance_column = nil

  validates :type, presence: true, length: { maximum: 50 }
  validates :name, presence: true, length: { maximum: 200 }
  validates :description, presence: true, length: { maximum: 500 }
  validates :parameters, exclusion: { in: [ nil ] }

  belongs_to :user, inverse_of: :reports
  belongs_to :website, inverse_of: :reports
end
