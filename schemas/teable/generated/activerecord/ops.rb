class Ops < ApplicationRecord
  attribute :id, default: -> { Cuid.generate }

  validates :collection, presence: true, uniqueness: { scope: %i[doc_id version] }
  validates :doc_id, presence: true
  validates :doc_type, presence: true
  validates :version, presence: true
  validates :operation, presence: true
  validates :created_by, presence: true
end
