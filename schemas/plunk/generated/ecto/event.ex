defmodule Plunk.Event do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true}
  @foreign_key_type :binary_id
  @timestamps_opts [type: Plunk.PrismaDateTime, autogenerate: {Plunk.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          name: String.t(),
          data: map() | nil,
          inserted_at: DateTime.t(),
          project: Plunk.Project.t() | nil,
          contact: Plunk.Contact.t() | nil,
          email: Plunk.Email.t() | nil
        }

  schema "events" do
    field(:name, :string)
    field(:data, :map)
    field(:project_id, :binary_id, source: :projectId)
    field(:contact_id, :binary_id, source: :contactId)
    field(:email_id, :binary_id, source: :emailId)
    belongs_to(:project, Plunk.Project, foreign_key: :project_id, define_field: false)
    belongs_to(:contact, Plunk.Contact, foreign_key: :contact_id, define_field: false)
    belongs_to(:email, Plunk.Email, foreign_key: :email_id, define_field: false)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end