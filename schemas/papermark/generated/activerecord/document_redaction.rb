class DocumentRedaction < ApplicationRecord
  self.table_name = "DocumentRedaction"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :source, default: "AI"
  attribute :status, default: "PENDING"
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :pageNumber, presence: true
  validates :x, presence: true
  validates :y, presence: true
  validates :width, presence: true
  validates :height, presence: true

  belongs_to :job, class_name: "DocumentRedactionJob", foreign_key: "jobId", inverse_of: :redactions
end
