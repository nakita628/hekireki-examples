class FileRecord < ApplicationRecord
  self.table_name = "File"
  self.inheritance_column = nil
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { SecureRandom.uuid }
  alias_attribute :created_at, :createdAt

  validates :name, presence: true
  validates :type, presence: true
  validates :s3Key, presence: true

  belongs_to :user, foreign_key: "userId", inverse_of: :files
end
