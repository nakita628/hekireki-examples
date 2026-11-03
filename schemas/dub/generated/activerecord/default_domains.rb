class DefaultDomains < ApplicationRecord
  self.table_name = "DefaultDomains"

  attribute :id, default: -> { Cuid.generate }
  attribute :dubsh, default: true
  attribute :chatgpt, default: true
  attribute :sptifi, default: true
  attribute :gitnew, default: true
  attribute :callink, default: true
  attribute :amznid, default: true
  attribute :ggllink, default: true
  attribute :figpage, default: true
  attribute :loooooooong, default: false

  validates :projectId, uniqueness: true

  belongs_to :project, foreign_key: "projectId", inverse_of: :default_domains
end
