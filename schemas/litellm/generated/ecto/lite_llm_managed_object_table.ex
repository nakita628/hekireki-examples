defmodule LiteLLM.LiteLLMManagedObjectTable do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true}
  @foreign_key_type :binary_id
  @timestamps_opts [type: LiteLLM.PrismaDateTime, autogenerate: {LiteLLM.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          unified_object_id: String.t(),
          model_object_id: String.t(),
          file_object: map(),
          file_purpose: String.t(),
          status: String.t() | nil,
          batch_processed: boolean(),
          created_by: String.t() | nil,
          team_id: String.t() | nil,
          org_id: String.t() | nil,
          api_key: String.t() | nil,
          request_tags: map() | nil,
          end_user_id: String.t() | nil,
          updated_by: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "LiteLLM_ManagedObjectTable" do
    field(:unified_object_id, :string)
    field(:model_object_id, :string)
    field(:file_object, :map)
    field(:file_purpose, :string)
    field(:status, :string)
    field(:batch_processed, :boolean, default: false)
    field(:created_by, :string)
    field(:team_id, :string)
    field(:org_id, :string)
    field(:api_key, :string)
    field(:request_tags, :map)
    field(:end_user_id, :string)
    field(:updated_by, :string)
    timestamps(inserted_at_source: :created_at)
  end
end