class Folder < ApplicationRecord
  self.table_name = "Folder"
  self.inheritance_column = nil

  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :type, { default: "default", mega: "mega" }, default: :default, validate: true
  enum :accessLevel, { read: "read", write: "write" }, validate: { allow_nil: true }

  validates :name, presence: true, uniqueness: { scope: :projectId }

  belongs_to :project, foreign_key: "projectId", inverse_of: :folders
  has_one :dashboard, foreign_key: "folderId", inverse_of: :folder, dependent: :destroy
  has_many :links, foreign_key: "folderId", inverse_of: :folder, dependent: :nullify
  has_many :users, class_name: "FolderUser", foreign_key: "folderId", inverse_of: :folder, dependent: :destroy
  has_many :access_requests, class_name: "FolderAccessRequest", foreign_key: "folderId", inverse_of: :folder,
                             dependent: :destroy
  has_many :webhooks, class_name: "FolderWebhook", foreign_key: "folderId", inverse_of: :folder, dependent: :destroy
end
