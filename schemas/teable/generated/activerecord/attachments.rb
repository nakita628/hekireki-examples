class Attachments < ApplicationRecord
  attribute :id, default: -> { Cuid.generate }

  validates :token, presence: true, uniqueness: true
  validates :hash, presence: true
  validates :size, presence: true
  validates :mimetype, presence: true
  validates :path, presence: true
  validates :created_by, presence: true
end
