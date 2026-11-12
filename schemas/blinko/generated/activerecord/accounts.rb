class Accounts < ApplicationRecord
  attribute :name, default: ""
  attribute :nickname, default: ""
  attribute :password, default: ""
  attribute :image, default: ""
  attribute :apiToken, default: ""
  attribute :description, default: ""
  attribute :note, default: 0
  attribute :role, default: ""
  attribute :loginType, default: ""
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  has_many :notes, class_name: "Notes", foreign_key: "accountId", inverse_of: :account, dependent: :nullify
  has_many :configs, foreign_key: "userId", inverse_of: :user, dependent: :nullify
  has_many :tags, foreign_key: "accountId", inverse_of: :account, dependent: :nullify
  has_many :comments, class_name: "Comments", foreign_key: "accountId", inverse_of: :account, dependent: :nullify
  has_many :attachments, class_name: "Attachments", foreign_key: "accountId", inverse_of: :account, dependent: :nullify
  has_many :follows, class_name: "Follows", foreign_key: "accountId", inverse_of: :account,
                     dependent: :restrict_with_error
  has_many :notifications, class_name: "Notifications", foreign_key: "accountId", inverse_of: :account,
                           dependent: :restrict_with_error
  has_many :conversations, foreign_key: "accountId", inverse_of: :account, dependent: :restrict_with_error
  has_many :shared_notes, class_name: "NoteInternalShare", foreign_key: "accountId", inverse_of: :account,
                          dependent: :restrict_with_error
  has_many :ai_scheduled_tasks, foreign_key: "accountId", inverse_of: :account, dependent: :restrict_with_error
end
