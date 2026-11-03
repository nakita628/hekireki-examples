class BatchExport < ApplicationRecord
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }

  validates :user_id, presence: true
  validates :name, presence: true
  validates :status, presence: true
  validates :query, exclusion: { in: [ nil ] }
  validates :format, presence: true

  belongs_to :project, inverse_of: :batch_export
end
